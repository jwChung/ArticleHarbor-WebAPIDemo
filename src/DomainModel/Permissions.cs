namespace ArticleHarbor.DomainModel
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
        /// The create article permission
        /// </summary>
        CreateArticle = 2,

        /// <summary>
        /// The delete own article permission
        /// </summary>
        DeleteArticle = 4,

        /// <summary>
        /// The delete any article permission
        /// </summary>
        DeleteAnyArticle = 8,

        /// <summary>
        /// The modify own article permission
        /// </summary>
        ModifyArticle = 0x10,

        /// <summary>
        /// The modify any article permission
        /// </summary>
        ModifyAnyArticle = 0x20,

        /// <summary>
        /// The create
        /// </summary>
        Create = 0x40,

        /// <summary>
        /// The delete
        /// </summary>
        Delete = 0x80,

        /// <summary>
        /// The delete any
        /// </summary>
        DeleteAny = 0x100,

        /// <summary>
        /// The modify
        /// </summary>
        Modify = 0x200,

        /// <summary>
        /// The modify any
        /// </summary>
        ModifyAny = 0x400,

        /// <summary>
        /// The user permissions
        /// </summary>
        UserPermissions = Authentication,

        /// <summary>
        /// The author permissions
        /// </summary>
        AuthorPermissions = UserPermissions
            | Create | Delete | Modify
            | CreateArticle | DeleteArticle | ModifyArticle,

        /// <summary>
        /// The administrator permissions
        /// </summary>
        AdministratorPermissions = AuthorPermissions
            | DeleteAny | ModifyAny
            | DeleteAnyArticle | ModifyAnyArticle
    }
}