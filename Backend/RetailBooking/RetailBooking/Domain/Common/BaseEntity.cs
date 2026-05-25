using System;

namespace RetailOrdering.Domain.Common
{
    /// <summary>
    /// Base entity providing common properties for all domain entities.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Primary key identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// UTC timestamp when the entity was created. Defaults to current UTC time.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// UTC timestamp when the entity was last updated. Null when the entity has not been updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Sets the UpdatedAt timestamp to the current UTC time.
        /// Use this method when updating the entity to record modification time.
        /// </summary>
        public void TouchUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
