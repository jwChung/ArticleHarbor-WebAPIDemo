namespace ArticleHarbor.DomainModel.Models
{
    public enum Role
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The user
        /// </summary>
        User = Permissions.UserPermissions,

        /// <summary>
        /// The author
        /// </summary>
        Author = Permissions.AuthorPermissions,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = Permissions.AdministratorPermissions
    }
}