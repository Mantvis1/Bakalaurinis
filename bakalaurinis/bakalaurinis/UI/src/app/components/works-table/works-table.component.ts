import { Component, OnInit, ViewChild } from "@angular/core";
import { WorkService } from "../../services/work.service";
import { AuthenticationService } from "../../services/authentication.service";
import { GetWork } from "../../models/get-work";
import { MatDialog, MatPaginator, MatTableDataSource } from "@angular/material";
import { NewWork } from "../../models/new-work";
import { InviteUserComponent } from '../invite-user/invite-user.component';
import { SettingsService } from 'src/app/services/settings.service';
import { WorkFormComponent } from '../work-form/work-form.component';
import { WorkReviewComponent } from '../work-review/work-review.component';
import { FilterService } from 'src/app/services/filter.service';
import { ConvertToStringService } from 'src/app/services/convert-to-string.service';

@Component({
  selector: "app-works-table",
  templateUrl: "./works-table.component.html",
  styleUrls: ["./works-table.component.css"]
})

export class WorksTableComponent implements OnInit {
  works = new MatTableDataSource<GetWork>();
  isRowClicked = true;
  workToEdit: NewWork = new NewWork();
  newWork: NewWork = new NewWork();

  displayedColumns: string[] = [
    "Title",
    "Priority",
    "Edit",
    "Invite",
    "Delete"
  ];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private workService: WorkService,
    private authenticationService: AuthenticationService,
    private dialog: MatDialog,
    private settingsService: SettingsService,
    private filterService: FilterService,
    public convertToStringService: ConvertToStringService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authenticationService.getUserId());
    this.refreshTable();
  }

  deleteById(id: number) {
    this.updateRowClick(false);

    if (confirm("Do you want to delete current?")) {
      this.workService.delete(id).subscribe(() => {
        this.refreshTable();
        this.updateRowClick(true);
      });
    }
  }

  refreshTable() {
    this.workService.getAllByUserId(this.authenticationService.getUserId()).subscribe(
      data => {
        this.works.data = data;
        this.updateDataSource();
        this.works.paginator = this.paginator;
        this.works.filterPredicate = this.filterTable;
      });
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    );
  }

  openCreateModal() {
    this.updateRowClick(false);

    const dialogRef = this.dialog.open(WorkFormComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        formTitle: "New work",
        activityFormData: this.newWork,
        formConfirmationButtonName: "Create"
      }
    });

    dialogRef.afterClosed().subscribe(newWork => {
      this.updateRowClick(true);

      if (newWork) {
        newWork.userId = this.authenticationService.getUserId();

        this.workService.create(newWork).subscribe(
          () => {
            this.refreshTable();
            this.newWork = new newWork();

          }
        );
      }
    });
  }

  editFrom(element: NewWork) {
    this.updateRowClick(false);
    this.workToEdit = Object.assign({}, element);

    const dialogRef = this.dialog.open(WorkFormComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        formTitle: "Edit work",
        activityFormData: this.workToEdit,
        formConfirmationButtonName: "Edit"
      }
    });

    dialogRef.afterClosed().subscribe(editActivity => {
      this.updateRowClick(true);

      if (editActivity) {
        this.workService.update(editActivity, editActivity.id).subscribe(
          () => {
            this.refreshTable();
          }
        );
      }
    });
  }

  invite(workId: number) {
    this.updateRowClick(false);

    const dialogRef = this.dialog.open(InviteUserComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        senderId: this.authenticationService.getUserId(),
        workId: workId,
        receiverName: ''
      }
    });

    dialogRef.afterClosed().subscribe(() => { this.updateRowClick(true); })
  }

  applyFilter(value: string): void {
    this.works.filter = this.filterService.getFilteredValue(value);
  }

  onRowClicked(workId: number): void {
    if (this.isRowClicked) {
      this.dialog.open(WorkReviewComponent, {
        minWidth: "300px",
        width: "50%",
        data: {
          workId: workId
        }
      });
    }
  }

  private updateRowClick(isRowCanBeClicked: boolean): void {
    this.isRowClicked = isRowCanBeClicked;
  }

  private updateDataSource() {
    this.works.data.forEach(work => {
      work.priorityString = this.convertToStringService.getWorkPriorityByIndex(work.activityPriority);
    });
  }

  private filterTable(work: GetWork, filterText: string): boolean {
    return (work.title && work.title.toLowerCase().indexOf(filterText) >= 0) ||
      (work.priorityString && work.priorityString.toLowerCase().indexOf(filterText) >= 0);
  }
}
