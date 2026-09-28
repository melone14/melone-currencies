import { apiClient } from './apiClient';

export interface CurrencyRate {
  name: string;
  code: string;
  value: number;
  effectiveDate: string;
}

export interface PagedResult<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export interface CurrencyHistoryPoint {
  date: string;
  value: number;
}

export interface CurrencyHistory {
  code: string;
  name: string;
  rates: CurrencyHistoryPoint[];
}

export async function getLatestCurrencies(
  pageNumber: number,
  pageSize: number,
  signal?: AbortSignal
) {
  const response = await apiClient.get<PagedResult<CurrencyRate>>('/currencies/latest', {
    params: { pageNumber, pageSize },
    signal,
  });
  return response.data;
}

export async function getCurrencyHistory(
  code: string,
  fromDate: Date,
  toDate: Date,
  signal?: AbortSignal
) {
  const response = await apiClient.get<CurrencyHistory | null>(
    `/currencies/history/${code}`,
    {
      params: {
        fromDate: fromDate.toISOString(),
        toDate: toDate.toISOString(),
      },
      signal,
    }
  );
  return response.data;
}