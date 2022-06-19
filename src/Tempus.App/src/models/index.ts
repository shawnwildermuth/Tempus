export interface WorkerEntity {
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