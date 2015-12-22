/***************************************************************************
 *                     CustomRemoteAdminPacketHandlers.cs
 *                            -------------------
 *   begin                : May 19, 2010
 *   copyright            : (C) Antony Ho
 *   email                : ntonyworkshop@gmail.com
 *   website              : http://antonyho.net/
 *
 *   Copyright (C) 2011 Ho Man Chung
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *   
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *   GNU General Public License for more details.
 *   
 *   You should have received a copy of the GNU General Public License
 *   along with this program. If not, see <http://www.gnu.org/licenses/>.
 ***************************************************************************/

using System;
using Server;
using Server.Misc;
using Server.Network;

namespace Server.RemoteAdmin
{
    public static class CustomRemoteAdminPacketHandlers
    {
        public static void Configure()
        {
            RemoteAdminHandlers.Register( 0x41, new OnPacketReceive( Save ) );
            RemoteAdminHandlers.Register( 0x42, new OnPacketReceive( Shutdown ) );
            RemoteAdminHandlers.Register( 0x46, new OnPacketReceive( WorldBroadcast ) );
        }

        private static void WorldBroadcast( NetState state, PacketReader pvSrc )
        {
            string message = pvSrc.ReadUTF8String();
            int hue = pvSrc.ReadInt16();
            bool ascii = pvSrc.ReadBoolean();

            World.Broadcast(hue, ascii, message);

            state.Send( new MessageBoxMessage( "Your message has been broadcasted.", "Message Broadcasted" ) );
        }

        private static void Save( NetState state, PacketReader pvSrc )
        {
            AutoSave.Save();

            state.Send( new MessageBoxMessage( "World Save done.", "World Saved" ) );
        }

        private static void Shutdown( NetState state, PacketReader pvSrc )
        {
            bool restart = pvSrc.ReadBoolean();
            bool save = pvSrc.ReadBoolean();

            Console.WriteLine( "RemoteAdmin: shutting down server (Restart: {0}) (Save: {1}) [{2}]", restart, save, DateTime.Now );

            if ( save && !AutoRestart.Restarting )
                AutoSave.Save();

            Core.Kill( restart );
        }
    }
}