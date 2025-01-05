export interface RegisterModel {
    email: string; // Required
    password: string; // Required
    name?: string;
    surname?: string;
    userName?: string;
    phoneNumber?: string;
    streetAndNumber?: string;
    postalCode?: string;
    city?: string;
    birthDate?: string;
    drivingLicenseNumber?: string;
    drivingLicenseIssueDate?: string;
    drivingLicenseExpirationDate?: string;
    personalNumber?: string;
    idCardIssuedBy?: string;
    idCardIssueDate?: string;
    idCardExpirationDate?: string;
  }