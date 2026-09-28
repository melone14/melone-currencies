import { useParams } from 'react-router-dom';
import { useCurrencyHistory } from '../hooks/useCurrencyHistory';
import CurrencyHistoryTable from '../components/CurrencyHistoryTable';
import { LoadingSpinner } from '../components/LoadingSpinner';
import ErrorMessage from '../components/ErrorMessage';
import { BackButton } from '../components/BackButton';

function CurrencyView() {
  const { code } = useParams<{ code: string }>();

  const { history, loading, error } = useCurrencyHistory(code ?? '');

  if (!code) return <ErrorMessage message="Brak kodu waluty w adresie URL." />;
  if (loading) return <LoadingSpinner />;
  if (error) return <ErrorMessage message={error} />;

  return (
    <div>
      <BackButton />

      {history && history.rates.length > 0 ? (
        <CurrencyHistoryTable history={history} />
      ) : (
        <div style={{ margin: '20px auto', width: '600px' }}>
          Brak danych historycznych dla tej waluty.
        </div>
      )}
    </div>
  );
}

export default CurrencyView;
