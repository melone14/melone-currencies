import {
  createContext,
  useContext,
  useEffect,
  useState,
  type ReactNode,
} from 'react';
import { useSearchParams } from 'react-router-dom';
import axios from 'axios';
import {
  getLatestCurrencies,
  type CurrencyRate,
  type PagedResult,
} from '../api/currencies';

interface CurrenciesContextValue {
  data: PagedResult<CurrencyRate> | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  setPageNumber: (page: number) => void;
}

const PAGE_SIZE = 12;

const CurrenciesContext = createContext<CurrenciesContextValue | undefined>(
  undefined,
);

export function CurrenciesProvider({ children }: { children: ReactNode }) {
  const [searchParams, setSearchParams] = useSearchParams();

  const pageNumber = Number(searchParams.get('page')) || 1;

  const [data, setData] = useState<PagedResult<CurrencyRate> | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const setPageNumber = (page: number) => {
    setSearchParams(page <= 1 ? {} : { page: String(page) });
  };

  useEffect(() => {
    const controller = new AbortController();

    const fetchCurrencies = async () => {
      try {
        setLoading(true);
        setError(null);

        const result = await getLatestCurrencies(
          pageNumber,
          PAGE_SIZE,
          controller.signal,
        );
        setData(result);
      } catch (err) {
        if (axios.isCancel(err)) return;

        if (axios.isAxiosError(err)) {
          setError(err.response?.data?.message || err.message);
        } else {
          setError('Wystąpił nieoczekiwany błąd');
        }
      } finally {
        setLoading(false);
      }
    };

    fetchCurrencies();

    return () => controller.abort();
  }, [pageNumber]);

  return (
    <CurrenciesContext.Provider
      value={{ data, loading, error, pageNumber, setPageNumber }}
    >
      {children}
    </CurrenciesContext.Provider>
  );
}

export function useCurrencies() {
  const context = useContext(CurrenciesContext);

  if (!context) {
    throw new Error(
      'useCurrencies musi być użyty wewnątrz <CurrenciesProvider>',
    );
  }

  return context;
}
