import { Component, OnInit, ViewChild } from '@angular/core';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { SettingsService } from 'src/app/services/settings.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages = new MatTableDataSource<Message>();
  pageSize = 0;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  displayedColumns: string[] = [
    "Title",
    "Text",
    "Time",
    "Delete"
  ];

  constructor(
    private messageService: MessageService,
    private authService: AuthServiceService,
    private datePipe: DatePipe,
    private settingsService: SettingsService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authService.getUserId());
    this.getUserMessages();
    this.messages.paginator = this.paginator;
  }

  getUserMessages(): void {
    this.messageService.getUserMessages(this.authService.getUserId()).subscribe(data => {
      this.messages.data = Object.assign([], data);
    })
  }

  deleteAll() {
    this.messageService.deleteUserAllMessagesById(this.authService.getUserId()).subscribe(
      () => {
        this.getUserMessages();
      }
    );
  }

  deleteById(messageId: number) {
    this.messageService.deleteUserMessagesById(this.authService.getUserId(), messageId).subscribe(
      () => {
        this.getUserMessages();
      }
    );
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    )
  }

  getDataString(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-dd HH:mm:ss');
  }

  applyFilter(filterValue: string): void {
    this.messages.filter = filterValue.trim().toLowerCase();
  }

}
