import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";

const routes: Routes = [
  { path: "activities", component: ActivitiesTableComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
