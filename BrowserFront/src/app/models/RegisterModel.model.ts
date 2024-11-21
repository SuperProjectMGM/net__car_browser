export interface RegisterModel {
    email: string; // Required
    password: string; // Required
    firstName?: string;
    secondName?: string;
    userName?: string;
    phoneNumber?: string;
    addressStreet?: string;
    postalCode?: string;
    city?: string;
    dateOfBirth?: string;
    drivingLicenseNumber?: string;
    drivingLicenseIssueDate?: string;
    drivingLicenseExpirationDate?: string;
    idCardNumber?: string;
    idCardIssuedBy?: string;
    idCardIssueDate?: string;
    idCardExpirationDate?: string;
  }