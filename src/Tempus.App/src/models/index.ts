export interface IEntity {
  id: number;
}

export interface WorkerEntity extends IEntity {
  id: number;
  userName: string;
  baseRate: number;
  firstName: string | null;
  lastName: string | null;
  email: string | null;
  phone: string | null;
}

export interface WorkersResults {
  count: number;
  results: Array<WorkerEntity>
}

export interface WorkTypeEntity extends IEntity {
  id: number;
  name: string;
  description: string;
  defaultRate: number;
}

export interface ContactEntity extends IEntity {
  id: number;
  title: string;
  firstName: string;
  middleName: string | null;
  lastName: string;
  phone: string | null;
  email: string | null;
}

export interface LocationEntity extends IEntity {
  id: number;
  lineOne: string;
  lineTwo: string | null;
  lineThree: string | null;
  city: string | null;
  stateProvince: string;
  postalCode: string;
  country: string | null;
}

export interface CustomerEntity extends IEntity {
    id: number;
    companyName: string;
    companyPhone: string;
    location: LocationEntity;
    contacts: ContactEntity[];
}