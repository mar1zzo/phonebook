/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PhonesComponent } from './phones.component';

describe('PhonesComponent', () => {
  let component: PhonesComponent;
  let fixture: ComponentFixture<PhonesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhonesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhonesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
