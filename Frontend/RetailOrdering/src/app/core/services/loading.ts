import { Injectable, signal, computed } from '@angular/core';
import { Observable, finalize } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Loading {
  private activeCount = signal(0);
  readonly isLoading = computed(() => this.activeCount() > 0);

  track<T>(observable: Observable<T>): Observable<T> {
    this.activeCount.update((value) => value + 1);
    return observable.pipe(
      finalize(() => this.activeCount.update((value) => Math.max(0, value - 1)))
    );
  }
}
