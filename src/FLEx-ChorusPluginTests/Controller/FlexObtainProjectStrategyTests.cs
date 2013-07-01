﻿using System;
using System.IO;
using FLEx_ChorusPlugin.Controller;
using FLEx_ChorusPlugin.Infrastructure;
using LibChorus.TestUtilities;
using NUnit.Framework;
using Palaso.TestUtilities;
using TriboroughBridge_ChorusPlugin;

namespace FLEx_ChorusPluginTests.Controller
{
	/// <summary>
	/// Test the FlexObtainProjectStrategy.
	/// </summary>
	[TestFixture]
	public class FlexObtainProjectStrategyTests
	{
		[Test, Ignore("Not doing it with the filter for now.")]
		public void AlreadyHaveProjectFiltersOutAttemptToCloneAgain()
		{
			using (var sueRepo = new RepositoryWithFilesSetup("Sue", SharedConstants.CustomPropertiesFilename, "contents"))
			{
				var fakeProjectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
				Utilities.SetProjectsPathForTests(fakeProjectDir);
				try
				{
					using (var tempDir = TemporaryFolder.TrackExisting(fakeProjectDir))
					{
						var sue = sueRepo.GetRepository();
						var extantDir = Path.Combine(fakeProjectDir, "extantmatchingrepo");
						Directory.CreateDirectory(extantDir);
						Directory.CreateDirectory(Path.Combine(fakeProjectDir, "norepo"));
						sue.CloneLocalWithoutUpdate(extantDir);
						var strat = new FlexObtainProjectStrategy();
						Assert.IsFalse(strat.ProjectFilter(sueRepo.ProjectFolder.Path));
					}
				}
				finally
				{
					Utilities.SetProjectsPathForTests(null);
				}
			}
		}

		[Test]
		public void DoNotHaveProjectDoesNotFilterOutRepo()
		{
			using (var sueRepo = new RepositoryWithFilesSetup("Sue", SharedConstants.CustomPropertiesFilename, "contents"))
			{
				var fakeProjectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
				Utilities.SetProjectsPathForTests(fakeProjectDir);
				Directory.CreateDirectory(fakeProjectDir);
				try
				{
					using (var tempDir = TemporaryFolder.TrackExisting(fakeProjectDir))
					{
						var extantDir = Path.Combine(fakeProjectDir, "extantmatchingrepo");
						Directory.CreateDirectory(extantDir);
						Directory.CreateDirectory(Path.Combine(fakeProjectDir, "norepo"));
						var strat = new FlexObtainProjectStrategy();
						Assert.IsTrue(strat.ProjectFilter(sueRepo.ProjectFolder.Path));
					}
				}
				finally
				{
					Utilities.SetProjectsPathForTests(null);
				}
			}
		}
	}
}