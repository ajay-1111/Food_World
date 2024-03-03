// <copyright file="UserRegistrationContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Food_World.Models.EFDBContext
{
    /// <summary>
    /// This class is for user registration DB context.
    /// </summary>
    public class UserRegistrationContext : IdentityUser
    {

        /// <summary>Gets or sets the FirstName.</summary>
        [StringLength(100)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the FirstName.</summary>
        [StringLength(100)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the Email.</summary>
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>Gets or sets the Telephone.</summary>
        [StringLength(100)]
        public string Telephone { get; set; }

        /// <summary>Gets or sets the Password.</summary>
        [StringLength(50)]
        public string Password { get; set; }
    }
}
