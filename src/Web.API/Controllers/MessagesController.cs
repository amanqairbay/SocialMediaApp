using System.Security.Claims;
using Core.DTOs.Message;
using Core.Errors;
using Core.RequestFeatures;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Extensions;
using Web.API.Helpers;

namespace Web.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/users/{userId}/[controller]")]
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessagesController(
            IMessageService messageService,
            IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        /// <summary>
        /// Gets and returns a message, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="userId">The current user identifier.</param>
        /// <param name="id">The message identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the message matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the message exists.</response>
        /// <response code="401">If the user is not logged in.</response>
        /// <response code="404">If the message doesn't exist.</response>
        [HttpGet("{id}", Name = "GetMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessage(long userId, long id)
        {
            var message = await _messageService.GetMessageByIdAsync(userId, id);

            return Ok(message);
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="userId">The sender user identifier.</param>
        /// <param name="messageForCreationDto">The message data to create for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a message, its identifier, and the sender's identifier.
        /// </returns>
        /// <response code="200">If the message successfully created.</response>
        /// <response code="400">If the message could not be created.</response>
        /// <response code="401">If the user is not logged in.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateMessage(long userId, MessageForCreationDto messageForCreationDto)
        {
            var (messageId, senderId, message) = await _messageService.CreateMessage(userId, messageForCreationDto);

            return CreatedAtRoute("GetMessage", new { id = messageId, userId = senderId }, message);
        }

        /// <summary>
        /// Gets and returns a list of messages by parameters.
        /// </summary>
        /// <param name="userId">The current user identifier.</param>
        /// <param name="messageParams">The message params to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of messages.
        /// </returns>
        /// <response code="200">If the message is returned.</response>
        /// <response code="401">If the user is not logged in.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<IActionResult> GetMessageForUser(long userId, [FromQuery]MessageParameters messageParams)
        {
            var (messages, metaData) = await _messageService.GetMessageForUser(userId, messageParams);

            Response.AddPagination(metaData.CurrentPage, metaData.PageSize, metaData.TotalCount, metaData.TotalPages);

            return Ok(messages);
        }

        /// <summary>
        /// Gets and returns a message thread, if any, that has the specified <paramref name="userId" /> and <paramref name="recipientId" />.
        /// </summary>
        /// <param name="userId">The sender user identifier to get for.</param>
        /// <param name="recipientId">The recipient identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the message thread the specified <paramref name="userId" /> and <paramref name="recipientId" /> if it exists.
        /// </returns>
        /// <response code="200">If the messages are returned.</response>
        /// <response code="401">If the sender user is not logged in.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(long userId, long recipientId)
        {
            var messageThread = await _messageService.GetMessageThread(userId, recipientId);

            return Ok(messageThread);
        }
    }
}

