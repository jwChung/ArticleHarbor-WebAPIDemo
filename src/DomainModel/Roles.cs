namespace DomainModel
{
    using System;

    [Flags]
    public enum Roles
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The user
        /// </summary>
        User = 1,

        /// <summary>
        /// The author
        /// </summary>
        Author = 2,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = 4
    }
}