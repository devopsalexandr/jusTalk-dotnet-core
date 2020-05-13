﻿using System;

namespace JusTalk.DAL
{
	public interface ITimestampable
	{
		DateTime CreatedAt { get; set; }

		DateTime UpdatedAt { get; set; }
	}
}
