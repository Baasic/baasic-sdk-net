using System;

namespace Baasic.Client.Model.Dynamic
{
    /// <summary>
    /// </summary>
    public partial class DynamicSchema : BuiltInModelBase
    {
        #region Properties

        public System.DateTime DateCreated { get; set; }

        public System.DateTime DateUpdated { get; set; }

        public string Description { get; set; }

        public bool EnforceSchemaValidation { get; set; }

        public SGuid Id { get; set; }

        public string Name { get; set; }

        public Owner Owner { get; set; }

        public SGuid OwnerId { get; set; }

        public string Schema { get; set; }

        public System.DateTime TimeStamp { get; set; }

        #endregion Properties
    }
}