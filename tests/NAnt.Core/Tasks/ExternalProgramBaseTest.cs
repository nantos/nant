// NAnt - A .NET build tool
// Copyright (C) 2001-2007 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

// Gert Driesen (drieseng@users.sourceforge.net)

using System;

using NUnit.Framework;

using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace Tests.NAnt.Core.Tasks {
    [TestFixture]
    public class ExternalProgramBaseTest {
        [Test]
        public void UseRuntimeEngine () {
            ExternalProgramBase prog;
            
            prog = new MockExternalProgram ();
            Assert.AreEqual (ManagedExecution.Default, prog.Managed, "#A2");
            Assert.AreEqual (ManagedExecution.Auto, prog.Managed, "#A4");
            Assert.AreEqual (ManagedExecution.Default, prog.Managed, "#A6");

            prog = new ManagedExternalProgram ();
            Assert.AreEqual (ManagedExecution.Auto, prog.Managed, "#B2");
            Assert.AreEqual (ManagedExecution.Default, prog.Managed, "#B4");
        }

        [Test]
        public void Managed () {
            ExternalProgramBase prog;
            
            prog = new MockExternalProgram ();
            Assert.AreEqual (ManagedExecution.Default, prog.Managed, "#A1");
            prog.Managed = ManagedExecution.Auto;
            Assert.AreEqual (ManagedExecution.Auto, prog.Managed, "#A2");
            prog.Managed = ManagedExecution.Default;
            Assert.AreEqual (ManagedExecution.Default, prog.Managed, "#A4");
            prog.Managed = ManagedExecution.Strict;
            Assert.AreEqual (ManagedExecution.Strict, prog.Managed, "#A6");
            Assert.AreEqual (ManagedExecution.Strict, prog.Managed, "#A8");

            prog = new ManagedExternalProgram ();
            Assert.AreEqual (ManagedExecution.Auto, prog.Managed, "#B1");
            prog.Managed = ManagedExecution.Strict;
            Assert.AreEqual (ManagedExecution.Strict, prog.Managed, "#B2");
            prog.Managed = ManagedExecution.Default;
            Assert.AreEqual (ManagedExecution.Default, prog.Managed, "#B4");
            Assert.AreEqual (ManagedExecution.Auto, prog.Managed, "#B6");
        }
    }

    class MockExternalProgram : ExternalProgramBase {
        public override string ProgramArguments {
            get { return "boo.exe"; }
        }
    }

    class ManagedExternalProgram : ExternalProgramBase {
        public override string ProgramArguments {
            get { return "boo.exe"; }
        }
    }
}
