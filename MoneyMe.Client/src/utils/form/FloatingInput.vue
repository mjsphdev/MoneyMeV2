<script lang="ts">
import { defineComponent,  ref, computed } from 'vue';
import type { PropType } from 'vue';

export default defineComponent({
  name: 'FloatingInput',
  props: {
    id: {
      type: String,
      required: true,
    },
    placeholder: {
      type: String,
      required: true,
    },
    modelValue: {
      type: String,
      default: '',
    },
    validation: {
      type: String as PropType<'none' | 'required'>,
      default: 'none',
    },
    errorMessage: {
      type: String,
      default: '',
    },
  },
  emits: ['update:modelValue'],
  setup(props, {emit}) {
    const isFocused = ref(false);
    const isValid = computed(() => props.validation === 'none' || (props.validation === 'required' && props.modelValue));
    const isInvalid = computed(() => props.validation === 'required' && !props.modelValue);

    emit('update:modelValue', props.modelValue);

    return {
      isFocused,
      isValid,
      isInvalid,
    };
  },
});
</script>


<template>
  <div class="form-floating">
    <input
      type="text"
      class="form-control"
      :class="{ 'is-valid': isValid, 'is-invalid': isInvalid }"
      :id="id"
      :placeholder="placeholder"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
      @focus="isFocused = true"
      @blur="isFocused = false"
    />
    <label :for="id" :class="{ 'active': isFocused || modelValue }">{{ placeholder }}</label>
    <div class="invalid-feedback" v-if="isInvalid">{{ errorMessage }}</div>
  </div>
</template>

<style scoped>
.form-floating .form-control:focus ~ label,
.form-floating .form-control:not(:placeholder-shown) ~ label,
.form-floating.focused .form-control ~ label {
  top: 0;
  left: 0;
  font-size: 0.8rem;
  opacity: 1;
  color: #212529;
}

select.form-control,
input[type="text"].form-control,
input[type="email"].form-control  {
  border-top: none;
  border-left: none;
  border-right: none;
  border-radius: 0;
  border-bottom: 1px solid #b6b6b6;
}

select.form-control:focus,
input[type="text"].form-control:focus,
input[type="email"].form-control:focus  {
  box-shadow: none;
}
.selectLabel{
  color: #00d1cf;
  font-weight: normal;
}

</style>