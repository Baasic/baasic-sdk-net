using Baasic.Client.Model;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Notifications
{
    /// <summary>
    /// Notifications Module Client contract.
    /// </summary>
    public interface INotificationClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" />.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        Task<bool> PublishAsync(Notification notification);

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" />.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Notification" />.</typeparam>
        /// <param name="notification">The notification.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        Task<bool> PublishAsync<T>(T notification) where T : Notification;

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" /> s.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        Task<bool> PublishAsync(Notification[] notifications);

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Notification" />.</typeparam>
        /// <param name="notifications">The notifications.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        Task<bool> PublishAsync<T>(T[] notifications) where T : Notification;

        #endregion Methods
    }
}