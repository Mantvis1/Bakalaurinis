import { Component, ViewChild, OnInit } from "@angular/core";
import { MatMenuTrigger } from "@angular/material/menu";
import { ToolbarComponent } from "./components/toolbars/toolbar/toolbar.component";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = "UI";

  /*@ViewChild(ToolbarComponent, null) trigger: MatMenuTrigger;

  ngOnInit() {
    this.trigger.menuOpen();
  }*/
}
