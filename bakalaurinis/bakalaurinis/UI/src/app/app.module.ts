import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { ToolbarComponent } from "./components/toolbars/toolbar/toolbar.component";
import { ToolbarAfterLogInComponent } from "./components/toolbars/toolbar-after-log-in/toolbar-after-log-in.component";

import { MatMenuModule } from "@angular/material/menu";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import {
  MatDialogModule,
  MatInputModule,
  MatButtonModule,
  MatToolbarModule
} from "@angular/material";

import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    ToolbarAfterLogInComponent,
    LoginComponent,
    RegistrationComponent,
    ActivitiesTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatMenuModule,
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    AppRoutingModule
  ],
  entryComponents: [LoginComponent, RegistrationComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
