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