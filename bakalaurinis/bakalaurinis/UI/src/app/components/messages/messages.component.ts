import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[] = [];

  constructor(
    private messageService: MessageService,
    private authService: AuthServiceService
  ) { }

  ngOnInit() {
    this.getUserMessages();
  }

  getUserMessages(): void {
    this.messageService.getUserMessages(this.authService.getUserId()).subscribe(data => {
      this.messages = Object.assign([], data);
    })
  }

  deleteAll() {
    console.log("Not implemented");
  }

}
