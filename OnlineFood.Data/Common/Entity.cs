using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Data.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        #region Implementation of IEntity<T>

        /// <summary>
        /// Gets or sets The Id assigned to this entity set to typeof(T)
        /// </summary>
        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public virtual T Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entity is currently active.
        /// </summary>
        [Required]
        [ScaffoldColumn(false)]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entity has been marked as deleted. (Soft Delete)
        /// </summary>
        [Required]
        [ScaffoldColumn(false)]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when this entity has been created. 
        /// </summary>
        [Required]
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        #endregion
    }
}
