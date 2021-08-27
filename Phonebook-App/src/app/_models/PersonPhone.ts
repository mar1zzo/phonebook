import { PhoneNumberType } from './PhoneNumberType';

export interface PersonPhone {

    id: number;
    phoneNumber: string;
    phoneNumberTypeID: number;
    phoneNumberType: PhoneNumberType[];
}
