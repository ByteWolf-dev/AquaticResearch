export interface User {
    username: string;
    password: string;
  }
  
  export interface AuthResponse {
    accessToken: string;
    username: string;
  }

  export interface ObservationDto {
    notes: string;
    observationDateTime: Date;
    researchers: string;
    equipment: string;
    location: string;
}

export interface ResearchProjectDto {
  title: string;
  species: Species;
}

export interface Species {
  name: string;
  scientificName: string;
}