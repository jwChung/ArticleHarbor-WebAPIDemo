namespace ArticleHarbor.DomainModel.Models
{
    using System;

    [Flags]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This name is appropriate in this project.")]
    public enum Permissions
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The authentication permission
        /// </summary>
        Authentication = 1,
        
        /// <summary>
        /// The create
        /// </summary>
        Create = 2,

        /// <summary>
        /// The delete
        /// </summary>
        Delete = 4,

        /// <summary>
        /// The delete any
        /// </summary>
        DeleteAny = 8,

        /// <summary>
        /// The modify
        /// </summary>
        Modify = 0x10,

        /// <summary>
        /// The modify any
        /// </summary>
        ModifyAny = 0x20,

        /// <summary>
        /// The user permissions
        /// </summary>
        UserPermissions = Authentication,

        /// <summary>
        /// The author permissions
        /// </summary>
        AuthorPermissions = UserPermissions
            | Create | Delete | Modify,

        /// <summary>
        /// The administrator permissions
        /// </summary>
        AdministratorPermissions = AuthorPermissions
            | DeleteAny | ModifyAny
    }
}