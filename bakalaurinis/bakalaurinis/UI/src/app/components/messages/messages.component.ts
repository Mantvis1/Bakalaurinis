import { Component, OnInit, ViewChild } from '@angular/core';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { SettingsService } from 'src/app/services/settings.service';
import { ConvertToStringService } from 'src/app/services/convert-to-string.service';
import { FilterService } from 'src/app/services/filter.service';

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
    private authenticationService: AuthenticationService,
    private convertToStringService: ConvertToStringService,
    private settingsService: SettingsService,
    private filterService: FilterService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authenticationService.getUserId());
    this.getAllMessages();
  }

  getAllMessages(): void {
    this.messageService.getUserMessages(this.authenticationService.getUserId()).subscribe(data => {
      this.messagesDataSource = new MatTableDataSource(data);
      this.messagesDataSource.paginator = this.paginator;
      this.messagesDataSource.filterPredicate = this.filterTable;
      this.updateDataSource();
    });
  }

  deleteAll() {
    if (confirm("Do you want to delete all messages?")) {
      this.messageService.deleteUserAllMessagesById(this.authenticationService.getUserId()).subscribe(
        () => {
          this.getAllMessages();
        }
      );
    }
  }

  deleteById(messageId: number) {
    if (confirm("Do you want to delete current message?")) {
      this.messageService.deleteUserMessagesById(this.authenticationService.getUserId(), messageId).subscribe(
        () => {
          this.getAllMessages();
        }
      );
    }
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    );
  }

  applyFilter(value: string): void {
    this.messagesDataSource.filter = this.filterService.getFilteredValue(value);

    if (this.messagesDataSource.paginator) {
      this.messagesDataSource.paginator.firstPage();
    }
  }

  updateDataSource() {
    this.messagesDataSource.data.forEach(message => {
      message.dataString = this.convertToStringService.getFullDateAndTime(message.createdAt);
    });
  }

  private filterTable(message: Message, filterText: string): boolean {
    return (message.dataString && message.dataString.toLowerCase().indexOf(filterText) >= 0) ||
      (message.text && message.text.toLowerCase().indexOf(filterText) >= 0) ||
      (message.title && message.title.toLowerCase().indexOf(filterText) >= 0);
  }

}
