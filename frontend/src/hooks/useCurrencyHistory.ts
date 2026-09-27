import { useEffect, useState } from 'react';
import axios from 'axios';
import { getCurrencyHistory, type CurrencyHistory } from '../api/currencies';

export function useCurrencyHistory(code: string) {
  const [history, setHistory] = useState<CurrencyHistory | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const controller = new AbortController();

    const fetchHistory = async () => {
      try {
        setLoading(true);
        setError(null);
        setHistory(null);

        const toDate = new Date();
        const fromDate = new Date();
        fromDate.setDate(fromDate.getDate() - 180);

        const data = await getCurrencyHistory(code, fromDate, toDate, controller.signal);
        setHistory(data);
      } catch (err) {
        if (axios.isCancel(err)) return;
        setError('Nie udało się pobrać historii kursu.');
      } finally {
        setLoading(false);
      }
    };

    fetchHistory();

    return () => controller.abort();
  }, [code]);

  return { history, loading, error };
}