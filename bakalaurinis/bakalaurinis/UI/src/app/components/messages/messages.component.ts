import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[] = [];

  constructor() { }

  ngOnInit() {
    this.messages.push({ id: 1, title: 'title', text: 'texts' });
    this.messages.push({ id: 2, title: 'sdfgh', text: 'cvbc' });
  }

  deleteAll() {
    console.log("Not implemented");
  }

}
