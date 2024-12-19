export interface UserProfile {
  email: string;
  phoneNumber: string;
  name: string;
  surname: string;
  birthDate?: string;
  drivingLicenseNumber: string;
  drivingLicenseIssueDate?: string;
  drivingLicenseExpirationDate?: string;
  streetAndNumber?: string;
  postalCode?: string;
  city?: string;
  personalNumber?: string;
  idCardIssuedBy?: string;
  idCardIssueDate?: string;
  idCardExpirationDate?: string;
}
