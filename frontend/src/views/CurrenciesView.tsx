import { useNavigate } from 'react-router-dom';
import { useCurrencies } from '../context/CurrenciesContext';
import { CurrencyListItem } from '../components/CurrencyListItem';
import { PaginationControls } from '../components/PaginationControls';
import { LoadingSpinner } from '../components/LoadingSpinner';
import ErrorMessage from '../components/ErrorMessage';

function CurrenciesListView() {
  const { data, loading, error, pageNumber, setPageNumber } = useCurrencies();
  const navigate = useNavigate();

  if (error) return <ErrorMessage message={error} />;
  if (!data) return null;

  const handleRowClick = (code: string) => {
    navigate(`/currencies/${code}`);
  };

  return (
    <div>
      <div style={{ width: '600px', margin: '30px auto', minHeight: '554px' }}>
        {loading ? (
          <LoadingSpinner />
        ) : (
          data.items.map((currency) => (
            <CurrencyListItem
              key={currency.code}
              currency={currency}
              onClick={handleRowClick}
            />
          ))
        )}

        <PaginationControls
          pageNumber={pageNumber}
          totalPages={data.totalPages}
          totalCount={data.totalCount}
          onPageChange={setPageNumber}
        />
      </div>
    </div>
  );
}

export default CurrenciesListView;
