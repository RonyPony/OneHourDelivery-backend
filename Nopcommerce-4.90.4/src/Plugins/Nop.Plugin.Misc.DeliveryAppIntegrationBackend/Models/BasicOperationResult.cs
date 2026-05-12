using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a generic result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BasicOperationResult<T> : IOperationResult<T>
    {
        private BasicOperationResult()
        {

        }

        private BasicOperationResult(string message, bool success, T entity, string messageDetail = "")
        {
            Message = message;
            Success = success;
            Entity = entity;
            MessageDetail = messageDetail;
        }

        /// <summary>
        /// Represents the message operation result
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represents the message operation result detail.
        /// </summary>
        public string MessageDetail { get; set; }

        /// <summary>
        /// Represents if the operation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Represents the operation result
        /// </summary>
        public T Entity { get; set; }


        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResult{T}"/> successful with the <see cref="T"/> default value
        /// </summary>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> successful</returns>
        public static BasicOperationResult<T> Ok()
        {
            return new BasicOperationResult<T>("", true, default(T));
        }

        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResult{T}"/> successfully
        /// </summary>
        /// <param name="entity">An instance of <see cref="T"/></param>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> successful</returns>
        public static BasicOperationResult<T> Ok(T entity)
        {
            return new BasicOperationResult<T>("", true, entity);
        }

        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResult{T}"/> for fail case.
        /// </summary>
        /// <param name="message">An <see cref="string"/> value that represents a error message</param>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> failed</returns>
        public static BasicOperationResult<T> Fail(string message)
        {
            return new BasicOperationResult<T>(message, false, default(T));
        }

        /// <summary>
        /// Creates an instance of <see cref="BasicOperationResult{T}"/> for fail case.
        /// </summary>
        /// <param name="message">An <see cref="string"/> value that represents a error message</param>
        /// <param name="errorDetail"> erro detail</param>
        /// <returns>An instance of <see cref="BasicOperationResult{T}"/> failed</returns>
        public static BasicOperationResult<T> Fail(string message, string errorDetail)
        {
            return new BasicOperationResult<T>(message, false, default(T), errorDetail);
        }

    }
}
