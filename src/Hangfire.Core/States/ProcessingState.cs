﻿// This file is part of Hangfire.
// Copyright © 2013-2014 Sergey Odinokov.
// 
// Hangfire is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// Hangfire is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with Hangfire. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;
using Hangfire.Common;

namespace Hangfire.States
{
    public class ProcessingState : IState
    {
        public static readonly string StateName = "Processing";

        public ProcessingState(string serverId, int workerNumber)
        {
            if (String.IsNullOrWhiteSpace(serverId)) throw new ArgumentNullException("serverId");

            ServerId = serverId;
            StartedAt = DateTime.UtcNow;
            WorkerNumber = workerNumber;
        }

        public DateTime StartedAt { get; set; }
        public string ServerId { get; set; }
        public int WorkerNumber { get; set; }

        public string Name { get { return StateName; } }
        public string Reason { get; set; }
        public bool IsFinal { get { return false; } }
        public bool IgnoreJobLoadException { get { return false; } }

        public Dictionary<string, object> SerializeData()
        {
            return new Dictionary<string, object>
            {
                { "StartedAt", JobHelper.SerializeDateTime(StartedAt) },
                { "ServerId", ServerId },
                { "WorkerNumber", WorkerNumber.ToString(CultureInfo.InvariantCulture) }
            };
        }
    }
}
