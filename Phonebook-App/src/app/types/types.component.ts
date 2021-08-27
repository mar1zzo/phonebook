import { Component, OnInit, TemplateRef } from '@angular/core';
import { PhoneNumberType } from '../_models/PhoneNumberType';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { TypeService } from '../_services/type.service';

@Component({
  selector: 'app-types',
  templateUrl: './types.component.html',
  styleUrls: ['./types.component.css']
})
export class TypesComponent implements OnInit {

  types: PhoneNumberType[] = [];
  type!: PhoneNumberType;
  typesFiltered: PhoneNumberType[] = [];
  modalRef!: BsModalRef;
  registerForm!: FormGroup;

  saveMode = 'post';
  bodyDeleteType = '';

  _listFilter = '';

  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.typesFiltered = this.listFilter ? this.filterTypes(this.listFilter) : this.types;
  }

  constructor(
    private typeService: TypeService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.validation();
    this.getTypes();
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  validation() {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.max(80)]]
    });
  }

  newPhoneNumberType(template: any) {
    this.saveMode = 'post';
    this.openModal(template);
  }

  updatePhoneNumberType(type: PhoneNumberType, template: any) {
    this.saveMode = 'put';
    this.openModal(template);
    this.type = type;
    this.registerForm.patchValue(type);
  }

  deletePhoneNumberType(type: PhoneNumberType, template: any) {
    this.openModal(template);
    this.type = type;
    this.bodyDeleteType = `Are you sure you want to delete the 'Type Name: ${type.name}'?`;
  }

  saveChange(template: any) {
    if (this.saveMode == 'post') {
      this.type = Object.assign({}, this.registerForm.value);
      this.typeService.postType(this.type).subscribe(
        _newType => {
          console.log(_newType);
          template.hide();
          this.getTypes();
        }, error => {
          console.log(error);
        });
    } else {
      this.type = Object.assign({ id: this.type.id }, this.registerForm.value);
      this.typeService.putType(this.type).subscribe(
        _newType => {
          console.log(_newType);
          template.hide();
          this.getTypes();
        }, error => {
          console.log(error);
        });
    }
  }

  confirmDelete(template: any) {
    this.typeService.deleteType(this.type.id).subscribe(
      () => {
        template.hide();
        this.getTypes();
      }, error => {
        console.log(error);
      }
    );
  }

  filterTypes(filterBy: string): PhoneNumberType[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.types.filter(type => type.name.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  getTypes() {
    this.typeService.getAllTypes().subscribe(
      (_types: PhoneNumberType[]) => {
        this.types = _types;
        this.typesFiltered = this.types;
      }, error => {
        console.log(error);
      });
  }
}
