namespace ArticleHarbor.DomainModel
{
    using System;

    [Flags]
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
        CreateGeneral = 0x40,

        /// <summary>
        /// The delete
        /// </summary>
        DeleteGeneral = 0x80,

        /// <summary>
        /// The delete any
        /// </summary>
        DeleteAnyGeneral = 0x100,

        /// <summary>
        /// The modify
        /// </summary>
        ModifyGeneral = 0x200,

        /// <summary>
        /// The modify any
        /// </summary>
        ModifyAnyGeneral = 0x400,

        /// <summary>
        /// The user permissions
        /// </summary>
        UserPermissions = Authentication,

        /// <summary>
        /// The author permissions
        /// </summary>
        AuthorPermissions = UserPermissions
            | CreateGeneral | DeleteGeneral | ModifyGeneral
            | CreateArticle | DeleteArticle | ModifyArticle,

        /// <summary>
        /// The administrator permissions
        /// </summary>
        AdministratorPermissions = AuthorPermissions
            | DeleteAnyGeneral | ModifyAnyGeneral
            | DeleteAnyArticle | ModifyAnyArticle
    }
}