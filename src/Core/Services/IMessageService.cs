using Core.DTOs.Message;
using Core.Entities;
using Core.RequestFeatures;

namespace Core.Services
{
    /// <summary>
    /// Represents the message service.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Gets and returns a message, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="userId">The sender user identifier.</param>
        /// <param name="id">The message identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the message matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        Task<Message> GetMessageByIdAsync(long userId, long id);

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="userId">The message data to create for.</param>
        /// <param name="messageForCreationDto">The message data to create for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a message, its identifier, and the sender's identifier.
        /// </returns>
        Task<(long messageId, long senderId, MessageForCreationDto messageForCreationDto)> CreateMessage(long userId, MessageForCreationDto messageForCreationDto);

        /// <summary>
        /// Gets and returns a list of messages by parameters.
        /// </summary>
        /// <param name="userId">The current user identifier.</param>
        /// <param name="messageParams">The message params to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of messages.
        /// </returns>
        Task<(IEnumerable<MessageToReturnDto> messages, MetaData metaData)> GetMessageForUser(long userId, MessageParameters messageParams);

        /// <summary>
        /// Gets and returns a message thread, if any, that has the specified <paramref name="userId" /> and <paramref name="recipientId" />.
        /// </summary>
        /// <param name="userId">The sender user identifier to get for.</param>
        /// <param name="recipientId">The recipient identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the message thread the specified <paramref name="userId" /> and <paramref name="recipientId" /> if it exists.
        /// </returns>
        Task<IEnumerable<MessageToReturnDto>> GetMessageThread(long userId, long recipientId);
    }
}

