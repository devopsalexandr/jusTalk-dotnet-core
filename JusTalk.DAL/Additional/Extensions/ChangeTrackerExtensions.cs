﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace JusTalk.DAL
{
	public static class ChangeTrackerExtensions
	{
		public static void TrackTimestampBeforeSaveChanges(this ChangeTracker changeTracker)
		{
			foreach(var entityEntry in changeTracker.Entries<ITimestampable>())
			{
				switch(entityEntry.State)
				{
					case EntityState.Added:
						entityEntry.Entity.CreatedAt = entityEntry.Entity.UpdatedAt = DateTime.Now; 
						break;
					
					case EntityState.Modified:
						entityEntry.Entity.UpdatedAt = DateTime.Now;
						break;
				}
			}
		}
	}
}
