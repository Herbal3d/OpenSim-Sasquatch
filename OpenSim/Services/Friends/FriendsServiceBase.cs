/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Microsoft.Extensions.Configuration;
using OpenSim.Data;
using OpenSim.Services.Base;

namespace OpenSim.Services.Friends
{
    public class FriendsServiceBase : ServiceBase
    {
        protected IFriendsData m_Database = null;
        protected string m_ConfigName = "FriendsService";

        public FriendsServiceBase(IConfiguration config) : base(config)
        {
            string dllName = String.Empty;
            string connString = String.Empty;

            //
            // Try reading the [FriendsService] section first, if it exists
            //
            var friendsConfig = config.GetSection(m_ConfigName);
            if (friendsConfig.Exists())
            {
                dllName = friendsConfig.GetValue("StorageProvider", dllName);
                connString = friendsConfig.GetValue("ConnectionString", connString);
            }

            //
            // Try reading the [DatabaseService] section, if it exists
            //
            var dbConfig = config.GetSection("DatabaseService");
            if (dbConfig.Exists())
            {
                if (string.IsNullOrEmpty(dllName))
                    dllName = dbConfig.GetValue("StorageProvider", String.Empty);
                if (string.IsNullOrEmpty(connString))
                    connString = dbConfig.GetValue("ConnectionString", String.Empty);
            }

            //
            // We tried, but this doesn't exist. We can't proceed.
            //
            if (string.IsNullOrEmpty(dllName))
                throw new Exception($"No StorageProvider configured for {m_ConfigName}");

            string realm = "Friends";
            if (friendsConfig != null)
                realm = friendsConfig.GetValue("Realm", realm);

            m_Database = LoadPlugin<IFriendsData>(dllName, new Object[] { connString, realm });
            if (m_Database == null)
            {
                throw new Exception(
                    $"Could not find a storage interface IFriendsData in the given StorageProvider {dllName}");
            }
        }
    }
}
