import { Component, OnInit, TemplateRef } from '@angular/core';
import { PhoneService } from '../_services/phone.service';
import { PersonPhone } from '../_models/PersonPhone';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { PhoneNumberType } from '../_models/PhoneNumberType';

@Component({
  selector: 'app-phones',
  templateUrl: './phones.component.html',
  styleUrls: ['./phones.component.css']
})
export class PhonesComponent implements OnInit {

  phones: PersonPhone[] = [];
  phone!: PersonPhone;
  types: PhoneNumberType[] = [];
  phonesFiltered: PersonPhone[] = [];
  modalRef!: BsModalRef;
  registerForm!: FormGroup;

  saveMode = 'post';
  bodyDeletePhone = '';

  _listFilter = '';

  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.phonesFiltered = this.listFilter ? this.filterPhones(this.listFilter) : this.phones;
  }

  constructor(
    private phoneService: PhoneService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.validation();
    this.getPhones();
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  validation() {
    this.registerForm = this.fb.group({
      phoneNumber: ['', [Validators.required, Validators.max(20)]],
      phoneNumberTypeID: ['', Validators.required]
    });
  }

  newPersonPhone(template: any) {
    this.saveMode = 'post';
    this.openModal(template);
  }

  updatePersonPhone(phone: PersonPhone, template: any) {
    this.saveMode = 'put';
    this.openModal(template);
    this.phone = phone;
    this.registerForm.patchValue(phone);
  }

  deletePersonPhone(phone: PersonPhone, template: any) {
    this.openModal(template);
    this.phone = phone;
    this.bodyDeletePhone = `Are you sure you want to delete the 'Phone: ${phone.phoneNumber}'?`;
  }

  saveChange(template: any) {
    if (this.saveMode == 'post') {
      this.phone = Object.assign({}, this.registerForm.value);
      this.phoneService.postPhone(this.phone).subscribe(
        _newPhone => {
          console.log(_newPhone);
          template.hide();
          this.getPhones();
        }, error => {
          console.log(error);
        });
    } else {
      this.phone = Object.assign({ id: this.phone.id }, this.registerForm.value);
      this.phoneService.putPhone(this.phone).subscribe(
        _newPhone => {
          console.log(_newPhone);
          template.hide();
          this.getPhones();
        }, error => {
          console.log(error);
        });
    }
  }

  confirmDelete(template: any) {
    this.phoneService.deletePhone(this.phone.id).subscribe(
      () => {
        template.hide();
        this.getPhones();
      }, error => {
        console.log(error);
      }
    );
  }

  filterPhones(filterBy: string): PersonPhone[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.phones.filter(phone => phone.phoneNumber.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  getPhones() {
    this.phoneService.getAllPhones().subscribe(
      (_phones: PersonPhone[]) => {
        this.phones = _phones;
        this.phonesFiltered = this.phones;
        console.log(this.phones);
      }, error => {
        console.log(error);
      });
  }
}
