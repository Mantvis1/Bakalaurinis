import { Component, OnInit, ViewChild } from '@angular/core';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';
import { SettingsService } from 'src/app/services/settings.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messagesDataSource = new MatTableDataSource<Message>();

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
  }

  getUserMessages(): void {
    this.messageService.getUserMessages(this.authService.getUserId()).subscribe(data => {
      this.messagesDataSource = new MatTableDataSource(data);
      this.messagesDataSource.paginator = this.paginator;
      this.messagesDataSource.filterPredicate = this.filterTable;
      this.updateDataSource();
    });
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
    );
  }

  getDataString(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-dd HH:mm:ss');
  }

  applyFilter(filterValue: string): void {
    this.messagesDataSource.filter = filterValue.trim().toLowerCase();

    if (this.messagesDataSource.paginator) {
      this.messagesDataSource.paginator.firstPage();
    }
  }

  updateDataSource() {
    this.messagesDataSource.data.forEach(message => {
      message.dataString = this.getDataString(message.createdAt);
    });
  }

  private filterTable(message: Message, filterText: string): boolean {
    return (message.dataString && message.dataString.toLowerCase().indexOf(filterText) >= 0) ||
      (message.text && message.text.toLowerCase().indexOf(filterText) >= 0) ||
      (message.title && message.title.toLowerCase().indexOf(filterText) >= 0);
  }

}
