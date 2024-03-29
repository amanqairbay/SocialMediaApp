﻿using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the base class for entities
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public long Id { get; set; }
    }
}

