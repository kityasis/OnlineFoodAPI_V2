using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data.Common
{
    /// <summary>
    /// interface for Entity
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// Gets or sets The Id assigned to this entity set to typeof(T)
        /// </summary>
        T Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Boolean flag to indicate if this entity is currently active.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Boolean flag to indicate if this entity has been marked as deleted. (Soft Delete)
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when entity created.
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
}
