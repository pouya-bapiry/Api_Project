﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
