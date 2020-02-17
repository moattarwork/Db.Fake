import { writable } from 'svelte/store';

export const connection = writable(null);
export const databases = writable(null);
export const selectedDatabase = writable(null);
export const tables = writable(null);
export const tablesLoading = writable(false);
