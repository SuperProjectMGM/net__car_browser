export interface UserProfile {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  address: {
    street: string;
    postalCode: string;
    city: string;
  };
  dateOfBirth: string;
  drivingLicense: {
    number: string;
    issueDate: string;
    expirationDate: string;
  };
  idCard: {
    number: string;
    issuedBy: string;
    issueDate: string;
    expirationDate: string;
  };
}
