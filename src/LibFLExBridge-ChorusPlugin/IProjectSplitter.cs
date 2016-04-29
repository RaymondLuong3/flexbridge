﻿// Copyright (c) 2015 SIL International
// This software is licensed under the MIT License (http://opensource.org/licenses/MIT)
using System;
using Palaso.Progress;

namespace LibFLExBridgeChorusPlugin
{
	public interface IProjectSplitter
	{
		void PushHumptyOffTheWall(IProgress progress, bool verbose, string mainFilePathname);
	}
}
