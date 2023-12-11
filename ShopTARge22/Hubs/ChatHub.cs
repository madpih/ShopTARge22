﻿using Microsoft.AspNetCore.SignalR;

namespace ShopTARge22.Hubs;

public class ChatHub : Hub

	{
		public async Task SendMessage(string user, string message)
		{
		Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm"));
		}
	}
