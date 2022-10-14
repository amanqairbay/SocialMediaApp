using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Core.DTOs.Message;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.RequestFeatures;
using Core.Services;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    /// <summary>
    /// Represents the message service.
    /// </summary>
    public class MessageService : IMessageService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapper _mapper;
        #endregion Fields

        #region Constructor
        public MessageService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IRepository<Message> messageRepository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        #endregion Constructor

        /// <summary>
        /// Gets and returns a message, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="userId">The current user identifier.</param>
        /// <param name="id">The message identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the message matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown if message with <paramref name="id" /> doesn't exist in the database.</exception>
        /// <exception cref="UnauthorizedException">Thrown if the current user is not logged in.</exception>
        public async Task<Message> GetMessageByIdAsync(long userId, long id)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var messageFromDb = await _messageRepository.GetByIdAsync(id);

            if (messageFromDb is null)
                throw new NotFoundException($"Message with {id} doesn't exist in the database.");
            
            return messageFromDb;
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="userId">The sender user identifier.</param>
        /// <param name="messageForCreationDto">The message data to create for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a message, its identifier, and the sender's identifier.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown if the current user is not logged in.</exception>
        /// <exception cref="BadRequestException">Thrown if the message could not be saved.</exception>
        public async Task<(long messageId, long senderId, MessageForCreationDto messageForCreationDto)> CreateMessage(long userId, MessageForCreationDto messageForCreationDto)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            messageForCreationDto.SenderId = userId;

            var message = _mapper.Map<Message>(messageForCreationDto);

            _messageRepository.Add(message);

            var messageToReturn = _mapper.Map<MessageForCreationDto>(message);

            if (await _messageRepository.SaveAll() is not true)
                throw new BadRequestException($"Creating the message failed on save.");

            return (messageId: message.Id, senderId: message.SenderId, messageForCreationDto: messageToReturn);
        }

        /// <summary>
        /// Gets and returns a list of messages by parameters.
        /// </summary>
        /// <param name="userId">The current user identifier.</param>
        /// <param name="messageParams">The message params to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of messages.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown if the current user is not logged in.</exception>
        public async Task<(IEnumerable<MessageToReturnDto> messages, MetaData metaData)> GetMessageForUser(long userId, MessageParameters messageParams)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not logged in.");

            var specification = new MessageSpecification(messageParams);
            var totalItems = await _messageRepository.CountAsync(specification);
            var messagesFromDb = await _messageRepository.GetAllWithSpecificationAsync(specification);
            var messagesDto = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromDb);

            var pagedList = new PagedList<MessageToReturnDto>((List<MessageToReturnDto>)messagesDto, totalItems, messageParams.PageIndex, messageParams.PageSize);

            return (messages: pagedList.Items, metaData: pagedList.MetaData);
        }

        /// <summary>
        /// Gets and returns a message thread, if any, that has the specified <paramref name="userId" /> and <paramref name="recipientId" />.
        /// </summary>
        /// <param name="userId">The sender user identifier to get for.</param>
        /// <param name="recipientId">The recipient identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the message thread the specified <paramref name="userId" /> and <paramref name="recipientId" /> if it exists.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown if the current user is not logged in.</exception>
        public async Task<IEnumerable<MessageToReturnDto>> GetMessageThread(long userId, long recipientId)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var specification = new MessageForThreadSpecification(userId, recipientId);
            var messagesFromDb = await _messageRepository.GetAllWithSpecificationAsync(specification);
            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromDb);

            return (messageThread);
        }
    }
}

