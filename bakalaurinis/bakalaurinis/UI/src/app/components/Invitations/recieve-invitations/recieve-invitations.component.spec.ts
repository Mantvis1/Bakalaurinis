import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecieveInvitationsComponent } from './recieve-invitations.component';

describe('RecieveInvitationsComponent', () => {
  let component: RecieveInvitationsComponent;
  let fixture: ComponentFixture<RecieveInvitationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecieveInvitationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecieveInvitationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
