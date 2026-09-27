import type { CurrencyRate } from '../../api/currencies';
import { RowWrapper } from './CurrencyListItem.style';

interface CurrencyListItemProps {
  currency: CurrencyRate;
  onClick: (code: string) => void;
}

export function CurrencyListItem({ currency, onClick }: CurrencyListItemProps) {
  return (
    <RowWrapper onClick={() => onClick(currency.code)}>
      <div>{currency.code} - {currency.name}</div>
      <div>{currency.value}</div>
    </RowWrapper>
  );
}