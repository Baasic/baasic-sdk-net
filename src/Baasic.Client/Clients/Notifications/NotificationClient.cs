using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using System;
using System.Net;
using System.Threading.Tasks;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Modules.Notifications
{
    /// <summary>
    /// Notification Module Client.
    /// </summary>
    public class NotificationClient : ClientBase, INotificationClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public NotificationClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the baasic client factory.
        /// </summary>
        /// <value>The baasic client factory.</value>
        protected virtual IBaasicClientFactory BaasicClientFactory { get; set; }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected override string ModuleRelativePath
        {
            get { return "notifications"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" />.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        public virtual Task<bool> PublishAsync(Notification notification)
        {
            return PublishAsync<Notification>(notification);
        }

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" />.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Notification" />.</typeparam>
        /// <param name="notification">The notification.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        public virtual async Task<bool> PublishAsync<T>(T notification) where T : Notification
        {
            if (String.IsNullOrWhiteSpace(notification.TemplateId))
            {
                throw new ArgumentNullException("TemplateId");
            }
            if (notification.Channels == null || notification.Channels.Length == 0)
            {
                throw new ArgumentNullException("Channels");
            }

            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var httpStatusCode = await client.PostAsync<Notification, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/publish/", ModuleRelativePath)), notification);
                return httpStatusCode == HttpStatusCode.Created;
            }
        }

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" /> s.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        public virtual Task<bool> PublishAsync(Notification[] notifications)
        {
            return PublishAsync<Notification>(notifications);
        }

        /// <summary>
        /// Asynchronously schedule publish for specified <see cref="Notification" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Notification" />.</typeparam>
        /// <param name="notifications">The notifications.</param>
        /// <returns>True if <see cref="Notification" /> is scheduled for publish, false otherwise.</returns>
        public virtual async Task<bool> PublishAsync<T>(T[] notifications) where T : Notification
        {
            foreach (var notification in notifications)
            {
                if (String.IsNullOrWhiteSpace(notification.TemplateId))
                {
                    throw new ArgumentNullException("TemplateId");
                }
                if (notification.Channels == null || notification.Channels.Length == 0)
                {
                    throw new ArgumentNullException("Channels");
                }
            }

            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var httpStatusCode = await client.PostAsync<Notification[], HttpStatusCode>(client.GetApiUrl(String.Format("{0}/publish/batch/", ModuleRelativePath)), notifications);
                return httpStatusCode == HttpStatusCode.Created;
            }
        }

        #endregion Methods
    }
}