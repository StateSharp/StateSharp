﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Event;
using System;

namespace UnitTests.Core.Event.Dictionary
{
    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void AddUser()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            server.State.RemotePlayers.SubscribeOnChange(moq.Object);
            server.State.RemotePlayers.Add("User1");
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}