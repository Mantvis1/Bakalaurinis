import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolbarAfterLogInComponent } from './toolbar-after-log-in.component';

describe('ToolbarAfterLogInComponent', () => {
  let component: ToolbarAfterLogInComponent;
  let fixture: ComponentFixture<ToolbarAfterLogInComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ToolbarAfterLogInComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolbarAfterLogInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
